class PublicTypebot < ApplicationRecord
  self.table_name = "PublicTypebot"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :typebotId, uniqueness: true
  validates :groups, exclusion: { in: [ nil ] }
  validates :variables, exclusion: { in: [ nil ] }
  validates :edges, exclusion: { in: [ nil ] }
  validates :theme, exclusion: { in: [ nil ] }
  validates :settings, exclusion: { in: [ nil ] }

  belongs_to :typebot, foreign_key: "typebotId", inverse_of: :published_typebot
  has_many :media_ids_cache, class_name: "RuntimeMediaIdCache", foreign_key: "publicTypebotId",
                             inverse_of: :public_typebot, dependent: :destroy
end
