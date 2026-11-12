class AiModels < ApplicationRecord
  self.table_name = "aiModels"

  attribute :sortOrder, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :title, presence: true, length: { maximum: 255 }
  validates :modelKey, presence: true, length: { maximum: 255 }
  validates :capabilities, exclusion: { in: [ nil ] }

  belongs_to :provider, class_name: "AiProviders", foreign_key: "providerId", inverse_of: :models
end
