class AnnotationScore < ApplicationRecord
  self.table_name = "AnnotationScore"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :active, default: true
  attribute :global, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :dataType, { categorical: "CATEGORICAL", boolean: "BOOLEAN", likert: "LIKERT", option: "OPTION", checkbox: "CHECKBOX" },
       validate: { allow_nil: true }

  validates :name, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :annotation_scores
  has_many :annotation_queue_scores, class_name: "AnnotationQueueScores", foreign_key: "annotationScoreId",
                                     inverse_of: :annotation_score, dependent: :restrict_with_error
end
