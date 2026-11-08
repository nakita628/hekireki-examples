class ImageUpload < ApplicationRecord
  self.table_name = "ImageUpload"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :url, presence: true, length: { maximum: 256 }

  belongs_to :user, foreign_key: "userId", inverse_of: :image_upload
end
