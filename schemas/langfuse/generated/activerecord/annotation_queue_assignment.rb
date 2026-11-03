class AnnotationQueueAssignment < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :project_id, uniqueness: { scope: %i[queue_id user_id] }

  belongs_to :project, inverse_of: :annotation_queue_assignment
  belongs_to :user, inverse_of: :annotation_queue_assignment
  belongs_to :queue, class_name: "AnnotationQueue", inverse_of: :annotation_queue_assignment
end
