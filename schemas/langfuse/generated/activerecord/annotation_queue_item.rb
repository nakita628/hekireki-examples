class AnnotationQueueItem < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :object_type, { trace: "TRACE", observation: "OBSERVATION", session: "SESSION" }, validate: true
  enum :status, { pending: "PENDING", completed: "COMPLETED" }, default: :pending, validate: true

  validates :object_id, presence: true

  belongs_to :queue, class_name: "AnnotationQueue", inverse_of: :annotation_queue_item
  belongs_to :locked_by_user, class_name: "User", optional: true, inverse_of: :annotated_locked_item
  belongs_to :annotator_user, class_name: "User", optional: true, inverse_of: :annotated_completed_item
  belongs_to :project, inverse_of: :annotation_queue_item
end
