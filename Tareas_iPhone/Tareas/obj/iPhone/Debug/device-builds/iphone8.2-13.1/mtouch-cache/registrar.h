#pragma clang diagnostic ignored "-Wdeprecated-declarations"
#pragma clang diagnostic ignored "-Wtypedef-redefinition"
#pragma clang diagnostic ignored "-Wobjc-designated-initializers"
#pragma clang diagnostic ignored "-Wunguarded-availability-new"
#define DEBUG 1
#include <stdarg.h>
#include <objc/objc.h>
#include <objc/runtime.h>
#include <objc/message.h>
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <MapKit/MapKit.h>

@class UIApplicationDelegate;
@class AppDelegate;
@class Buscar_Ubic_ViewController;
@class UITableViewSource;
@class Tareas_Nueva_Buscar_Buscar_Ubic_TableSource;
@class Crear_Tarea_ViewController;
@class Tareas_Nueva_Crear_Crear_Tarea_TableSource;
@class Crear_Tarea_TableViewCell1;
@class Crear_Tarea_TableViewCell2;
@class Crear_Tarea_TableViewCell3;
@class Crear_Tarea_TableViewCell4;
@class Add_Tarea_ViewController;
@class Datos_Tarea_ViewController;
@class Tareas_Tareas_Datos_Datos_Tarea_TableSource;
@class Datos_Tarea_TableViewCell;
@class Filtrar_Tareas_ViewController;
@class Lista_Tareas_ViewController;
@class Tareas_Tareas_Lista_Lista_Tareas_TableSource;
@class Lista_Tareas_TableViewCell;
@class UIKit_UIControlEventProxy;
@class Foundation_NSDispatcher;
@class __MonoMac_NSSynchronizationContextDispatcher;
@class Foundation_NSAsyncDispatcher;
@class __MonoMac_NSAsyncSynchronizationContextDispatcher;
@class NSURLSessionDataDelegate;
@class System_Net_Http_NSUrlSessionHandler_WrappedNSInputStream;
@class __NSObject_Disposer;
@class System_Net_Http_NSUrlSessionHandler_NSUrlSessionHandlerDelegate;

@interface UIApplicationDelegate : NSObject<UIApplicationDelegate> {
}
	-(id) init;
@end

@interface AppDelegate : NSObject<UIApplicationDelegate, UIApplicationDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIWindow *) window;
	-(void) setWindow:(UIWindow *)p0;
	-(BOOL) application:(UIApplication *)p0 didFinishLaunchingWithOptions:(NSDictionary *)p1;
	-(void) applicationWillResignActive:(UIApplication *)p0;
	-(void) applicationDidEnterBackground:(UIApplication *)p0;
	-(void) applicationWillEnterForeground:(UIApplication *)p0;
	-(void) applicationDidBecomeActive:(UIApplication *)p0;
	-(void) applicationWillTerminate:(UIApplication *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface Buscar_Ubic_ViewController : UIViewController {
}
	@property (nonatomic, assign) UIBarButtonItem * btnSave;
	@property (nonatomic, assign) UITableView * table;
	@property (nonatomic, assign) UITextField * txtSearch;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIBarButtonItem *) btnSave;
	-(void) setBtnSave:(UIBarButtonItem *)p0;
	-(UITableView *) table;
	-(void) setTable:(UITableView *)p0;
	-(UITextField *) txtSearch;
	-(void) setTxtSearch:(UITextField *)p0;
	-(void) viewDidLoad;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface UITableViewSource : NSObject<UIScrollViewDelegate, UIScrollViewDelegate> {
}
	-(id) init;
@end

@interface Tareas_Nueva_Buscar_Buscar_Ubic_TableSource : NSObject<UIScrollViewDelegate, UIScrollViewDelegate, UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface Crear_Tarea_ViewController : UIViewController {
}
	@property (nonatomic, assign) UIBarButtonItem * btnAdd;
	@property (nonatomic, assign) UIBarButtonItem * btnCancel;
	@property (nonatomic, assign) UITableView * tabla;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIBarButtonItem *) btnAdd;
	-(void) setBtnAdd:(UIBarButtonItem *)p0;
	-(UIBarButtonItem *) btnCancel;
	-(void) setBtnCancel:(UIBarButtonItem *)p0;
	-(UITableView *) tabla;
	-(void) setTabla:(UITableView *)p0;
	-(void) viewDidLoad;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface Tareas_Nueva_Crear_Crear_Tarea_TableSource : NSObject<UIScrollViewDelegate, UIScrollViewDelegate, UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface Crear_Tarea_TableViewCell1 : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * lblNombre;
	@property (nonatomic, assign) UILabel * lblTel;
	@property (nonatomic, assign) UITextField * txtNombre;
	@property (nonatomic, assign) UITextField * txtTel;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) lblNombre;
	-(void) setLblNombre:(UILabel *)p0;
	-(UILabel *) lblTel;
	-(void) setLblTel:(UILabel *)p0;
	-(UITextField *) txtNombre;
	-(void) setTxtNombre:(UITextField *)p0;
	-(UITextField *) txtTel;
	-(void) setTxtTel:(UITextField *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface Crear_Tarea_TableViewCell2 : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * lblOp1;
	@property (nonatomic, assign) UILabel * lblOp2;
	@property (nonatomic, assign) UISwitch * switchOp1;
	@property (nonatomic, assign) UISwitch * switchOp2;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) lblOp1;
	-(void) setLblOp1:(UILabel *)p0;
	-(UILabel *) lblOp2;
	-(void) setLblOp2:(UILabel *)p0;
	-(UISwitch *) switchOp1;
	-(void) setSwitchOp1:(UISwitch *)p0;
	-(UISwitch *) switchOp2;
	-(void) setSwitchOp2:(UISwitch *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface Crear_Tarea_TableViewCell3 : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * lblDescrip;
	@property (nonatomic, assign) UITextView * txtDescrip;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) lblDescrip;
	-(void) setLblDescrip:(UILabel *)p0;
	-(UITextView *) txtDescrip;
	-(void) setTxtDescrip:(UITextView *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface Crear_Tarea_TableViewCell4 : UITableViewCell {
}
	@property (nonatomic, assign) UIButton * btnDirec;
	@property (nonatomic, assign) UIButton * btnPais;
	@property (nonatomic, assign) UIButton * btnProv;
	@property (nonatomic, assign) UILabel * lblCiudad;
	@property (nonatomic, assign) UILabel * lblCoords;
	@property (nonatomic, assign) UILabel * lblDirec;
	@property (nonatomic, assign) UILabel * lblInfo;
	@property (nonatomic, assign) UILabel * lblNum;
	@property (nonatomic, assign) UILabel * lblPais;
	@property (nonatomic, assign) UILabel * lblProv;
	@property (nonatomic, assign) MKMapView * map;
	@property (nonatomic, assign) UISwitch * switchCoords;
	@property (nonatomic, assign) UIButton * txtCiudad;
	@property (nonatomic, assign) UITextField * txtLat;
	@property (nonatomic, assign) UITextField * txtLng;
	@property (nonatomic, assign) UITextField * txtNum;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIButton *) btnDirec;
	-(void) setBtnDirec:(UIButton *)p0;
	-(UIButton *) btnPais;
	-(void) setBtnPais:(UIButton *)p0;
	-(UIButton *) btnProv;
	-(void) setBtnProv:(UIButton *)p0;
	-(UILabel *) lblCiudad;
	-(void) setLblCiudad:(UILabel *)p0;
	-(UILabel *) lblCoords;
	-(void) setLblCoords:(UILabel *)p0;
	-(UILabel *) lblDirec;
	-(void) setLblDirec:(UILabel *)p0;
	-(UILabel *) lblInfo;
	-(void) setLblInfo:(UILabel *)p0;
	-(UILabel *) lblNum;
	-(void) setLblNum:(UILabel *)p0;
	-(UILabel *) lblPais;
	-(void) setLblPais:(UILabel *)p0;
	-(UILabel *) lblProv;
	-(void) setLblProv:(UILabel *)p0;
	-(MKMapView *) map;
	-(void) setMap:(MKMapView *)p0;
	-(UISwitch *) switchCoords;
	-(void) setSwitchCoords:(UISwitch *)p0;
	-(UIButton *) txtCiudad;
	-(void) setTxtCiudad:(UIButton *)p0;
	-(UITextField *) txtLat;
	-(void) setTxtLat:(UITextField *)p0;
	-(UITextField *) txtLng;
	-(void) setTxtLng:(UITextField *)p0;
	-(UITextField *) txtNum;
	-(void) setTxtNum:(UITextField *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface Add_Tarea_ViewController : UIViewController {
}
	@property (nonatomic, assign) UIButton * btnOp1;
	@property (nonatomic, assign) UIButton * btnOp2;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIButton *) btnOp1;
	-(void) setBtnOp1:(UIButton *)p0;
	-(UIButton *) btnOp2;
	-(void) setBtnOp2:(UIButton *)p0;
	-(void) viewDidLoad;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface Datos_Tarea_ViewController : UIViewController {
}
	@property (nonatomic, assign) UIButton * btnComplete;
	@property (nonatomic, assign) UITableView * tabla;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIButton *) btnComplete;
	-(void) setBtnComplete:(UIButton *)p0;
	-(UITableView *) tabla;
	-(void) setTabla:(UITableView *)p0;
	-(void) viewDidLoad;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface Tareas_Tareas_Datos_Datos_Tarea_TableSource : NSObject<UIScrollViewDelegate, UIScrollViewDelegate, UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface Datos_Tarea_TableViewCell : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * lblCalle;
	@property (nonatomic, assign) UILabel * lblCiudad;
	@property (nonatomic, assign) UILabel * lblCreador;
	@property (nonatomic, assign) UILabel * lblNombre;
	@property (nonatomic, assign) UILabel * lblNotas;
	@property (nonatomic, assign) UILabel * lblOp;
	@property (nonatomic, assign) UILabel * lblTelefono;
	@property (nonatomic, assign) MKMapView * map;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) lblCalle;
	-(void) setLblCalle:(UILabel *)p0;
	-(UILabel *) lblCiudad;
	-(void) setLblCiudad:(UILabel *)p0;
	-(UILabel *) lblCreador;
	-(void) setLblCreador:(UILabel *)p0;
	-(UILabel *) lblNombre;
	-(void) setLblNombre:(UILabel *)p0;
	-(UILabel *) lblNotas;
	-(void) setLblNotas:(UILabel *)p0;
	-(UILabel *) lblOp;
	-(void) setLblOp:(UILabel *)p0;
	-(UILabel *) lblTelefono;
	-(void) setLblTelefono:(UILabel *)p0;
	-(MKMapView *) map;
	-(void) setMap:(MKMapView *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface Filtrar_Tareas_ViewController : UIViewController {
}
	@property (nonatomic, assign) UIButton * btnMostrarSelect;
	@property (nonatomic, assign) UIButton * btnOrderSelect;
	@property (nonatomic, assign) UIButton * btnTipoSelect;
	@property (nonatomic, assign) UILabel * lblMostrar;
	@property (nonatomic, assign) UILabel * lblOrdenar;
	@property (nonatomic, assign) UILabel * lblTipo;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIButton *) btnMostrarSelect;
	-(void) setBtnMostrarSelect:(UIButton *)p0;
	-(UIButton *) btnOrderSelect;
	-(void) setBtnOrderSelect:(UIButton *)p0;
	-(UIButton *) btnTipoSelect;
	-(void) setBtnTipoSelect:(UIButton *)p0;
	-(UILabel *) lblMostrar;
	-(void) setLblMostrar:(UILabel *)p0;
	-(UILabel *) lblOrdenar;
	-(void) setLblOrdenar:(UILabel *)p0;
	-(UILabel *) lblTipo;
	-(void) setLblTipo:(UILabel *)p0;
	-(void) viewDidLoad;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface Lista_Tareas_ViewController : UIViewController {
}
	@property (nonatomic, assign) UIBarButtonItem * btnFiltrar;
	@property (nonatomic, assign) UITableView * table;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UIBarButtonItem *) btnFiltrar;
	-(void) setBtnFiltrar:(UIBarButtonItem *)p0;
	-(UITableView *) table;
	-(void) setTable:(UITableView *)p0;
	-(void) viewDidLoad;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface Tareas_Tareas_Lista_Lista_Tareas_TableSource : NSObject<UIScrollViewDelegate, UIScrollViewDelegate, UIScrollViewDelegate> {
}
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(NSInteger) tableView:(UITableView *)p0 numberOfRowsInSection:(NSInteger)p1;
	-(UITableViewCell *) tableView:(UITableView *)p0 cellForRowAtIndexPath:(NSIndexPath *)p1;
	-(BOOL) conformsToProtocol:(void *)p0;
	-(id) init;
@end

@interface Lista_Tareas_TableViewCell : UITableViewCell {
}
	@property (nonatomic, assign) UILabel * lblFecha;
	@property (nonatomic, assign) UILabel * lblInfo;
	@property (nonatomic, assign) UILabel * lblKms;
	@property (nonatomic, assign) UILabel * lblUbic;
	-(void) release;
	-(id) retain;
	-(int) xamarinGetGCHandle;
	-(void) xamarinSetGCHandle: (int) gchandle;
	-(UILabel *) lblFecha;
	-(void) setLblFecha:(UILabel *)p0;
	-(UILabel *) lblInfo;
	-(void) setLblInfo:(UILabel *)p0;
	-(UILabel *) lblKms;
	-(void) setLblKms:(UILabel *)p0;
	-(UILabel *) lblUbic;
	-(void) setLblUbic:(UILabel *)p0;
	-(BOOL) conformsToProtocol:(void *)p0;
@end

@interface NSURLSessionDataDelegate : NSObject<NSURLSessionDataDelegate, NSURLSessionTaskDelegate, NSURLSessionDelegate> {
}
	-(id) init;
@end


