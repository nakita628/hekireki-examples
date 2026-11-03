class Signatures < ApplicationRecord
  self.table_name = "Signatures"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :content, presence: true
  validates :autoAdd, inclusion: { in: [ true, false ] }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :signatures
end
