use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SocialMediaAgencyNiche")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "agencyId")]
    pub agency_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub niche: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::social_media_agency::Entity",
        from = "Column::AgencyId",
        to = "super::social_media_agency::Column::Id"
    )]
    Agency,
}

impl Related<super::social_media_agency::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Agency.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
