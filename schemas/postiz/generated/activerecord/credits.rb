class Credits < ApplicationRecord
  self.table_name = "Credits"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :type, default: "ai_images"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :credits, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :credits
end
