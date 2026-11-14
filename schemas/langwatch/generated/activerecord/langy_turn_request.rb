class LangyTurnRequest < ApplicationRecord
  self.table_name = "LangyTurnRequest"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :projectId, presence: true, uniqueness: { scope: [ :userId, :requestId ] }
  validates :userId, presence: true
  validates :requestId, presence: true
  validates :conversationId, presence: true
  validates :turnId, presence: true
  validates :status, presence: true
  validates :leaseOwner, presence: true
  validates :leaseExpiresAt, presence: true
end
