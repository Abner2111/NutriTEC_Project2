import React from 'react'
import './MiniCard.css'

function MiniCard({title, imageUrl, body, url}) {
  return (
    <div className='card-container1'>

        <div className='image-container1'>
            <img src={imageUrl} alt=''/>
        </div>

        <div className='card-content1'>
            <div className='card-title1'>
                {title}
            </div>
            <div className="card-body1">
                <p>{body}</p>
            </div>
        </div>
        
        <div className='btn1'>
            <button>
                <a href={url}>
                   GESTIONAR
                </a>
            </button>
        </div>
    </div>
  )
}

export default MiniCard