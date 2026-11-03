use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "oauth_app_secret")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub client_id: String,
    #[sea_orm(unique)]
    pub secret: String,
    pub masked_secret: String,
    pub created_time: DateTime,
    pub created_by: String,
    pub last_used_time: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::oauth_app::Entity",
        from = "Column::ClientId",
        to = "super::oauth_app::Column::ClientId",
        on_delete = "Cascade"
    )]
    OAuthApp,
    #[sea_orm(has_many = "super::oauth_app_token::Entity")]
    Tokens,
}

impl Related<super::oauth_app::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OAuthApp.def()
    }
}

impl Related<super::oauth_app_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tokens.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_time.is_not_set() {
            self.created_time = Set(now.naive_utc());
        }
        Ok(self)
    }
}
