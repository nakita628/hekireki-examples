class Plugin < ApplicationRecord
  self.table_name = "plugin"

  attribute :isUse, default: true
  attribute :isDev, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :metadata, exclusion: { in: [ nil ] }
  validates :path, presence: true
end
