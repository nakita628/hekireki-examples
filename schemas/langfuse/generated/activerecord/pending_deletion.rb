class PendingDeletion < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :is_deleted, default: false

  validates :object, presence: true
  validates :object_id, presence: true

  belongs_to :project, inverse_of: :pending_deletion
end
