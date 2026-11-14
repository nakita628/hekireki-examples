class PinnedTrace < ApplicationRecord
  self.table_name = "PinnedTrace"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt

  enum :source, { manual: "manual", share: "share" }, default: :manual, validate: true

  validates :traceId, presence: true, uniqueness: { scope: :projectId }

  belongs_to :project, foreign_key: "projectId", inverse_of: :pinned_traces
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :pinned_traces
end
