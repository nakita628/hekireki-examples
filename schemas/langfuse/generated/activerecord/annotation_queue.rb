class AnnotationQueue < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :score_config_ids, default: -> { [] }

  validates :name, presence: true, uniqueness: { scope: :project_id }

  belongs_to :project, inverse_of: :annotation_queue
  has_many :annotation_queue_item, class_name: "AnnotationQueueItem", foreign_key: "queue_id", inverse_of: :queue,
                                   dependent: :destroy
  has_many :annotation_queue_assignment, class_name: "AnnotationQueueAssignment", foreign_key: "queue_id",
                                         inverse_of: :queue, dependent: :destroy
end
