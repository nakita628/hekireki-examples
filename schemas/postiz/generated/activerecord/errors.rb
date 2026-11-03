class Errors < ApplicationRecord
  self.table_name = "Errors"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :body, default: "{}"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :message, presence: true
  validates :platform, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :errors
  belongs_to :post, foreign_key: "postId", inverse_of: :errors
end
