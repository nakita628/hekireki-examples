class AnnotationQueueItem < ApplicationRecord
  self.table_name = "AnnotationQueueItem"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :traceId, presence: true,
                      uniqueness: { scope: [ :userId, :projectId ], conditions: -> { where.not(userId: nil) } }
  validates :traceId,
            uniqueness: { scope: [ :annotationQueueId, :projectId ], conditions: -> { where.not(annotationQueueId: nil) } }

  belongs_to :annotation_queue, foreign_key: "annotationQueueId", optional: true, inverse_of: :annotation_queue_items
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :assigned_queue_items
  belongs_to :created_by_user, class_name: "User", foreign_key: "createdByUserId", optional: true,
                               inverse_of: :created_queue_items
  belongs_to :project, foreign_key: "projectId", inverse_of: :annotation_queue_item
end
