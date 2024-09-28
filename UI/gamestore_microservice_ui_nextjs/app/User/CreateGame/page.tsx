'use client'
import { CreateGame } from '@/app/api/game/gameActions';
import { getCurrentUser } from '@/app/authActions/authNext';
import React, { useEffect, useState } from 'react';
import {toast} from 'react-hot-toast'
function Page() {

  useEffect(()=>{
    const getUserAccessible = async () => {
         const user = await getCurrentUser();
        if (user?.role !== "SuperAdmin") {
            window.location.replace("/");
          
        }
    }
    getUserAccessible()

  },[])


    const [game,setGame] = useState({
        gameName:"",
        gameAuthor:"",
        price:0,
        videoFile:File,
        gameFile:File,
        gameDescription:"",
        minimumSystemRequirement:"",
        recommendedSystemRequirement:"",
        categoryId:""
    });

    const [videoToBeStore,setVideoStore] = useState<any>();
    const [gameFileToBeStore,setGameFileToBeStore] = useState<any>();
    const [status,setStatus] = useState(false);

    const handleCreate = async () => {
    var formData = new FormData();
        formData.append("GameName",game.gameName);
        formData.append("GameAuthor",game.gameAuthor);
        formData.append("Price",game.price.toString());
        formData.append("VideoFile",videoToBeStore);
        formData.append("GameFile",gameFileToBeStore);
        formData.append("GameDescription",game.gameDescription);
        formData.append("MinimumSystemRequirement",game.minimumSystemRequirement);
        formData.append("RecommendedSystemRequirement",game.recommendedSystemRequirement);
        formData.append("CategoryId",game.categoryId);


        const response = await CreateGame(formData);
        if (response.isSuccess) {
          toast.success("Game created successfully");
        }
        else{
          toast.error("Ooops something went wrong");

        }
        setStatus(response.isSuccess)
        console.log(response);
    }

    const handleFileChange = (e:React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files && e.target.files[0];
        if (file) {
            const imgType = file.type.split("/")[1];
            const validImgTypes = ["mp4","x-zip-compressed"]

            const imgValidateType = validImgTypes.filter((e) => {
                return e == imgType
            })

            if (imgValidateType.length === 0) {
                return alert("Unsuported media type")
            }

            const reader = new FileReader();
            reader.readAsDataURL(file);
            if (imgType === "mp4") {
                setVideoStore(file)
            }
            if (imgType === "x-zip-compressed") {
                setGameFileToBeStore(file)
            }
            
        }
    }

  return (
    <>
    <div className="w-1/2 mx-auto mt-10">
      <div className="mb-4">
        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="gamename">
          Oyun Adı
        </label>
        <input
          className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          id="gamename"
          type="text"
          placeholder="Oyun Adı"
          onChange={(e) => {setGame((prevState) => {return {...prevState,gameName:e.target.value}})}}
        />
      </div>
      <div className="mb-4">
        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="gameauthor">
          Oyun Yazarı
        </label>
        <input
          className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          id="gameauthor"
          type="text"
          placeholder="Oyun Yazarı"
          onChange={(e) => {setGame((prevState) => {return {...prevState,gameAuthor:e.target.value}})}}
        />
      </div>
      <div className="mb-4">
        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="price">
          Fiyat
        </label>
        <input
          className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          id="price"
          type="number"
          placeholder="Fiyat"
          onChange={(e) => {setGame((prevState) => {return {...prevState,price:parseFloat(e.target.value)}})}}
        />
      </div>
      <div className="mb-4">
        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="videofile">
          Video Dosyası
        </label>
        <input
          className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          id="videofile"
          type="file"
          onChange={handleFileChange}
        />
      </div>
      <div className="mb-4">
        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="gamefile">
          Oyun Dosyası
        </label>
        <input
          className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          id="gamefile"
          type="file"
          onChange={handleFileChange}
        />
      </div>
      <div className="mb-4">
        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="gamedescription">
          Oyun Açıklaması
        </label>
        <textarea
          className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          id="gamedescription"
          placeholder="Oyun Açıklaması"
          onChange={(e) => {setGame((prevState) => {return {...prevState,gameDescription:e.target.value}})}}
        ></textarea>
      </div>
      <div className="mb-4">
        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="minimumsystemrequirement">
          Minimum Sistem Gereksinimleri
        </label>
        <textarea
          className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          id="minimumsystemrequirement"
          placeholder="Minimum Sistem Gereksinimleri"
          onChange={(e) => {setGame((prevState) => {return {...prevState,minimumSystemRequirement:e.target.value}})}}
        ></textarea>
      </div>
      <div className="mb-4">
        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="recommendedsystemrequirement">
          Tavsiye Edilen Sistem Gereksinimleri
        </label>
        <textarea
          className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          id="recommendedsystemrequirement"
          placeholder="Tavsiye Edilen Sistem Gereksinimleri"
          onChange={(e) => {setGame((prevState) => {return {...prevState,recommendedSystemRequirement:e.target.value}})}}
        ></textarea>
      </div>
      <div className="mb-4">
        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="videofile">
          Kategori
        </label>
        <input
          className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
          id="categoryId"
          type="text"
          onChange={(e) => {setGame((prevState) => {return {...prevState,categoryId:e.target.value}})}}
        />
      </div>
      <div className="flex items-center justify-center">
        <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline" type="button" onClick={handleCreate} >
          Gönder
        </button>
      </div>
    </div>
    {
      status ? (
        <div>
            <h1>Game Image Added</h1>
        </div>
      ) : ""
    }
 
    </>
  );
}

export default Page