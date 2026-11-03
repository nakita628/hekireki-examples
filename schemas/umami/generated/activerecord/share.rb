class Share < ApplicationRecord
  self.table_name = "share"
  self.primary_key = "share_id"

  validates :entity_id, presence: true
  validates :name, presence: true, length: { maximum: 200 }
  validates :share_type, presence: true
  validates :slug, presence: true, length: { maximum: 100 }, uniqueness: true
  validates :parameters, exclusion: { in: [ nil ] }
end
