﻿namespace MGroup.MSolve.Discretization
{
	/// <summary>
	/// Defines the shape of a cell only. Since there are no dependencies, it can also be used to map corresponding cell/element 
	/// types from one module to another.
	/// Authors: Serafeim Bakalakos, Dimitris Tsapetis
	/// </summary>
	public enum CellType
	{
		// 0 ---- 1
		Line2,

		// 0----1----2
		Line3,

		// 3 ---- 2                         
		// |      |                            
		// |      |                            
		// 0 ---- 1
		Quad4,

		// 3 -- 6 -- 2
		// |         |
		// 7         5                            
		// |         |                            
		// 0 -- 4 -- 1                                                      
		Quad8,

		// 3 -- 6 -- 2
		// |    |    |
		// 7 -- 8 -- 5                            
		// |    |    |                            
		// 0 -- 4 -- 1                            
		Quad9,

		//    2
		//   /  \
		//  /    \                           
		// 0 ---  1                           
		Tri3,

		//     2
		//    /  \
		//   5    4
		//  /       \                            
		// 0 -- 3 -- 1                                                  
		Tri6,

		//             2   
		//           ,/|`\ 
		//         ,/  |  `\   
		//       ,/    '.   `\           
		//     ,/       |     `\     
		//   ,/         |       `\  
		//  0-----------'.--------1 
		//   `\.         |      ,/  
		//      `\.      |    ,/         
		//         `\.   '. ,/    
		//            `\. |/                                   
		//               `3
		Tet4,

		//            2
		//          ,/|`\                          
		//        ,/  |  `\
		//      ,6    '.   `5
		//    ,/       8     `\
		//  ,/         |       `\
		//  0--------4--'.--------1
		//  `\.         |      ,/
		//     `\.      |    ,9
		//        `7.   '. ,/
		//           `\. |/
		//              `3  
		Tet10,

		//  3----------2            
		//  |\         |\  
		//  | \        | \ 
		//  |  \       |  \ 
		//  |   7------+---6  
		//  |   |      |   |  
		//  0---+------1   |  
		//   \  |       \  |  
		//    \ |        \ |   
		//     \|         \|   
		//      4----------5 
		Hexa8,

		//  3----13----2     
		//   |\         |\      
		//  | 15       | 14   
		//  9  \       11 \    
		//  |   7----19+---6   
		//  |   |      |   |   
		//  0---+-8----1   |     
		//   \  17      \  18   
		//   10 |        12|     
		//     \|         \|   
		//      4----16----5   
		Hexa20,

		//  3----13----2     
		//   |\         |\    
		//   |15    24  | 14  
		//   9  \ 20    11 \  
		//   |   7----19+---6 
		//   |22 |  26  | 23| 
		//   0---+-8----1   | 
		//    \ 17    25 \  18
		//    10 |  21    12| 
		//      \|         \| 
		//       4----16----5
		Hexa27,


		//         3          
		//       ,/|`\       
		//     ,/  |  `\         
		//   ,/    |    `\        
		//  4------+------5      
		//  |      |      |     
		//  |      |      | 
		//  |      |      |    
		//  |      |      |     
		//  |      |      |     
		//  |      0      |  
		//  |    ,/ `\    |     
		//  |  ,/     `\  |         
		//  |,/         `\|       
		//  1-------------2    
		Wedge6,

		//         3            
		//       ,/|`\             
		//     12  |  13        
		//   ,/    |    `\       
		//  4------14-----5   
		//  |      8      |   
		//  |      |      |  
		//  |      |      |      
		//  |      |      |   
		//  10     |      11   
		//  |      0      |     
		//  |    ,/ `\    |   
		//  |  ,6     `7  |    
		//  |,/         `\|   
		//  1------9------2 
		Wedge15,

		//         3    
		//       ,/|`\  
		//     12  |  13    
		//   ,/    |    `\  
		//  4------14-----5 
		//  |      8      | 
		//  |    ,/|`\    | 
		//  |  15  |  16  | 
		//  |,/    |    `\| 
		//  10-----17-----11
		//  |      0      | 
		//  |    ,/ `\    | 
		//  |  ,6     `7  | 
		//  |,/         `\| 
		//  1------9------2 
		Wedge18,

		//                4          
		//              ,/|\         
		//            ,/ .'|\       
		//          ,/   | | \       
		//        ,/    .' | `\               
		//      ,/      |  '.  \          
		//    ,/       .'   |   \         
		//   /         |    |    \      
		// 0----------.'----3     \    
		//  `\        |      `\    \     
		//    `\     .'        `\ - \ 
		//      `\   |           `\  \     
		//        `\.'             `\`\      
		//          1------------------2    
		Pyra5,

		//                  4          
		//               ,/|\               
		//             ,/ .'|\               
		//           ,/   | | \                
		//         ,/    .' |  \           
		//       ,7      |  12  \         
		//     ,/       .'   |   \          
		//   ,/         9    |    11     
		//  0--------6-.'----3     \    
		//  `\        |      `\     \   
		//    `5     .'        10    \    
		//      `\   |           `\   \  
		//        `\.'              `\ \     
		//          1--------8----------2        
		Pyra13,

		//                4   
		//              ,/|\
		//            ,/ .'|\
		//          ,/   | | \
		//        ,/    .' | `.
		//      ,7      |  12  \
		//    ,/       .'   |   \
		//  ,/         9    |    11
		//  0--------6-.'----3    `.
		//   `\        |      `\    \
		//     `5     .' 13     10   \
		//       `\   |           `\  \ 
		//         `\.'             `\ \ 
		//           1--------8--------2
		Pyra14,

		Unknown
	}
}
