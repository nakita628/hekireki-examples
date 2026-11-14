class LangyActiveTurn < ApplicationRecord
  self.table_name = "LangyActiveTurn"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :projectId, presence: true, uniqueness: { scope: :conversationId }
  validates :conversationId, presence: true
  validates :turnId, presence: true
  validates :requestId, presence: true
  validates :userId, presence: true
  validates :status, presence: true
  validates :leaseOwner, presence: true
  validates :leaseExpiresAt, presence: true
end
