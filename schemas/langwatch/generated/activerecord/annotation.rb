class Annotation < ApplicationRecord
  self.table_name = "Annotation"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :traceId, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :annotations
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :annotation
end
