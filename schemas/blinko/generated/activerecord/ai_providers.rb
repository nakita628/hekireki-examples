class AiProviders < ApplicationRecord
  self.table_name = "aiProviders"

  attribute :sortOrder, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :title, presence: true, length: { maximum: 255 }
  validates :provider, presence: true, length: { maximum: 50 }
  validates :baseURL, length: { maximum: 500 }
  validates :apiKey, length: { maximum: 500 }

  has_many :models, class_name: "AiModels", foreign_key: "providerId", inverse_of: :provider, dependent: :destroy
end
