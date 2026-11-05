use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "invitation")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    pub email: String,
    pub role: Option<String>,
    pub status: String,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: DateTime,
    #[sea_orm(column_name = "inviterId")]
    pub inviter_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(
        belongs_to = "super::email_account::Entity",
        from = "Column::InviterId",
        to = "super::email_account::Column::Id",
        on_delete = "Cascade"
    )]
    Inviter,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::email_account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Inviter.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
