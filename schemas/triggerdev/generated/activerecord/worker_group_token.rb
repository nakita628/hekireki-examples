class WorkerGroupToken < ApplicationRecord
  self.table_name = "WorkerGroupToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :tokenHash, presence: true, uniqueness: true

  has_one :worker_group, class_name: "WorkerInstanceGroup", foreign_key: "tokenId", inverse_of: :token,
                         dependent: :destroy
end
