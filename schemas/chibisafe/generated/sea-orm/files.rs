use super::prisma_date_time::PrismaDateTime;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "files")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(unique)]
    pub uuid: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<i32>,
    pub name: String,
    pub original: String,
    pub r#type: String,
    pub size: String,
    pub hash: String,
    pub ip: String,
    #[sea_orm(column_name = "sourceUrl")]
    pub source_url: Option<String>,
    #[sea_orm(column_name = "isS3", default_value = false)]
    pub is_s3: bool,
    #[sea_orm(column_name = "isWatched", default_value = false)]
    pub is_watched: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: PrismaDateTime,
    #[sea_orm(column_name = "editedAt")]
    pub edited_at: Option<PrismaDateTime>,
    #[sea_orm(default_value = false)]
    pub quarantine: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::users::Entity",
        from = "Column::UserId",
        to = "super::users::Column::Id"
    )]
    User,
    #[sea_orm(has_one = "super::quarantine_files::Entity")]
    QuarantineFile,
}

impl Related<super::users::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::quarantine_files::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::QuarantineFile.def()
    }
}

impl Related<super::albums::Entity> for Entity {
    fn to() -> RelationDef {
        super::albums_tofiles::Relation::Albums.def()
    }
    fn via() -> Option<RelationDef> {
        Some(super::albums_tofiles::Relation::Files.def().rev())
    }
}

impl Related<super::tags::Entity> for Entity {
    fn to() -> RelationDef {
        super::files_totags::Relation::Tags.def()
    }
    fn via() -> Option<RelationDef> {
        Some(super::files_totags::Relation::Files.def().rev())
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.into());
        }
        Ok(self)
    }
}
