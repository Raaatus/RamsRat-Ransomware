from flask import Flask, request, jsonify
import os

app = Flask(__name__)

@app.route('/upload', methods=['POST'])
def upload_file():
    if 'file' not in request.files:
        return jsonify({"message": "No file part"}), 400

    files = request.files.getlist('file')
    if len(files) == 0:
        return jsonify({"message": "No files selected"}), 400

    for file in files:
        if file.filename == '':
            return jsonify({"message": "No selected file"}), 400

        folder_name = file.filename.split('.')[0]  # Récupère le début du nom de fichier avant le point
        UPLOAD_FOLDER = os.path.join(os.path.dirname(__file__), folder_name)

        if not os.path.exists(UPLOAD_FOLDER):
            os.makedirs(UPLOAD_FOLDER)

        file_path = os.path.join(UPLOAD_FOLDER, file.filename.replace(folder_name+".", "", 1))  # Supprime le nom du dossier du nom du fichier
        file.save(file_path)

    return jsonify({"message": "Files received successfully"}), 200

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)


