import win32com.client
import speech_recognition as sr
import pyttsx3
import os
from datetime import date
import time
import nltk
from nltk.tokenize import word_tokenize
import google.generativeai as genai
import webbrowser

import win32pipe
import win32file


genai.configure(api_key="AIzaSyCcSy1XJknbmO6qkLPzYgg3krvez8-Ui24")  # Remember to replace 'your_api_key_here' with your actual API key

speaker = win32com.client.Dispatch("SAPI.SpVoice")

today = str(date.today())

engine = pyttsx3.init()

engine.setProperty('rate', 190)  # speaking rate 
voices = engine.getProperty('voices')

engine.setProperty('voice', voices[1].id)  # 0 for male; 1 for female

today = str(date.today())
model = genai.GenerativeModel('gemini-pro')


def speak_text(text):
    engine.say(text)
    engine.runAndWait()

talk = []

def append2log(text):
    global today
    fname = 'chatlog-' + today + '.txt'
    with open(fname, "a") as f:
        f.write(text + "\n")

def open_gmail():
    webbrowser.open("https://mail.google.com")

def open_youtube():
    webbrowser.open("https://www.youtube.com")

def open_wikipedia():
    webbrowser.open("https://www.wikipedia.org")

def compose_email(recipient, email_content):
    # Create email URL with recipient and email content as parameters
    email_url = f"https://mail.google.com/mail/?view=cm&fs=1&to={recipient}&body={email_content}"

    # Open email URL in the default web browser
    webbrowser.open(email_url)

def main():
    global talk, today, rec, mic
    
    print("in main")  
    nltk.download('punkt')  # Download NLTK punkt tokenizer
    
    rec = sr.Recognizer()
    mic = sr.Microphone()
    rec.dynamic_energy_threshold = False
    rec.energy_threshold = 400  
    
    sleeping = True 
    
    pipe_name = r'\\.\pipe\testpipe'
    pipe_handle = win32file.CreateFile(
        pipe_name,
        win32file.GENERIC_WRITE,
        0,
        None,
        win32file.OPEN_EXISTING,
        0,
        None
    )
    
    while True: 
        print("before mic")    
        
        with mic as source1:            
            rec.adjust_for_ambient_noise(source1, duration=0.5)

            print("Listening ...")
            
            try: 
                audio = rec.listen(source1, timeout=10, phrase_time_limit=15)
               
                text = rec.recognize_google(audio)
                
                # AI is in sleeping mode
                if sleeping:
                    # User must start the conversation with the wake word "Jack"
                    if "jack" in text.lower():
                        request = text.lower().split("jack")[1]
                        
                        sleeping = False
                        append2log("_" * 40)
                        talk = []                        
                        today = str(date.today()) 
                        
                        if len(request) < 5:
                            speaker.Speak("Hi, there, how can I help?")
                            append2log("AI: Hi, there, how can I help?")
                            continue                      
                    else:
                        continue
                      
                # AI is awake         
                else: 
                    request = text.lower()

                    if "that's all" in request:
                        append2log(f"You: {request}\n")
                        speaker.Speak("Bye now")
                        append2log("AI: Bye now. \n")
                        print('Bye now')
                        sleeping = False
                        exit(0)
                    
                    if "jack" in request:
                        request = request.split("jack")[1]                        

                    # Process commands
                    if "open gmail" in request:
                        open_gmail()
                        append2log("You: Open Gmail\n")
                        append2log("AI: Opening Gmail\n")
                        speaker.Speak("Opening Gmail")
                        continue
                    
                    elif "write email to" in request:
                        recipient = request.split("to ")[1]
                        speaker.Speak(f"What would you like to write in the email to {recipient}?")
                        append2log(f"You: Write email to {recipient}\n")
                        with sr.Microphone() as source:
                            audio = rec.listen(source)
                        email_content=rec.recognize_google(audio)
                        compose_email(recipient, email_content)  # Compose email with drafted content
                        continue
                    
                    elif "open youtube" in request:
                        open_youtube()
                        append2log("You: Open YouTube\n")
                        append2log("AI: Opening YouTube\n")
                        speaker.Speak("Opening YouTube")
                        continue
                    
                    elif "open wikipedia" in request:
                        open_wikipedia()
                        append2log("You: Open Wikipedia\n")
                        append2log("AI: Opening Wikipedia\n")
                        speaker.Speak("Opening Wikipedia")
                        continue
                
                # process user's request (question)
                append2log(f"You: {request}\n ")

                print(f"You: {request}\n AI: " )

                talk.append({'role':'user', 'parts':[request]} )

                response = model.generate_content(talk, stream=True,
                # generation_config=genai.types.GenerationConfig(
                # Only one candidate for now.
                # max_output_tokens=125) 
                )
                win32file.WriteFile(pipe_handle, response.text)

                for chunk in response:
                    print(chunk.text, end='') 
                    speaker.speak(chunk.text.replace("*", ""))
                
                print(response.text)
                speaker.speak(response.text.replace("*", ""))
                print('\n')
                talk.append({'role':'model', 'parts':[response.text]})
                
                # print(talk[-1]['parts'][0])                

                append2log(f"AI: {response.text } \n")

            except Exception as e:
                continue 



if __name__ == "__main__":
    main()
