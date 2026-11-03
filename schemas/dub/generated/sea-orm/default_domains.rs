use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "DefaultDomains")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(default_value = true)]
    pub dubsh: bool,
    #[sea_orm(default_value = true)]
    pub chatgpt: bool,
    #[sea_orm(default_value = true)]
    pub sptifi: bool,
    #[sea_orm(default_value = true)]
    pub gitnew: bool,
    #[sea_orm(default_value = true)]
    pub callink: bool,
    #[sea_orm(default_value = true)]
    pub amznid: bool,
    #[sea_orm(default_value = true)]
    pub ggllink: bool,
    #[sea_orm(default_value = true)]
    pub figpage: bool,
    #[sea_orm(default_value = false)]
    pub loooooooong: bool,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
