use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "plugin")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub description: Option<String>,
    pub detail_desc: Option<String>,
    pub logo: String,
    pub help_url: Option<String>,
    pub status: String,
    pub positions: String,
    pub url: Option<String>,
    #[sea_orm(unique)]
    pub secret: String,
    pub masked_secret: String,
    pub i18n: Option<String>,
    pub config: Option<String>,
    pub plugin_user: Option<String>,
    pub created_time: DateTime,
    pub last_modified_time: Option<DateTime>,
    pub created_by: String,
    pub last_modified_by: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::plugin_install::Entity")]
    PluginInstall,
}

impl Related<super::plugin_install::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PluginInstall.def()
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
        if !self.last_modified_time.is_set() {
            self.last_modified_time = Set(Some(now.naive_utc()));
        }
        Ok(self)
    }
}
