class AnnotationQueue < ApplicationRecord
  self.table_name = "AnnotationQueue"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :slug, presence: true, uniqueness: { scope: :projectId }

  belongs_to :project, foreign_key: "projectId", inverse_of: :annotation_queue
  has_many :members, class_name: "AnnotationQueueMembers", foreign_key: "annotationQueueId",
                     inverse_of: :annotation_queue, dependent: :restrict_with_error
  has_many :annotation_queue_scores, class_name: "AnnotationQueueScores", foreign_key: "annotationQueueId",
                                     inverse_of: :annotation_queue, dependent: :restrict_with_error
  has_many :annotation_queue_items, foreign_key: "annotationQueueId", inverse_of: :annotation_queue, dependent: :nullify
end
