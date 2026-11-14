class AnnotationQueueScores < ApplicationRecord
  self.table_name = "AnnotationQueueScores"
  self.primary_key = %w[annotationQueueId annotationScoreId]

  belongs_to :annotation_score, foreign_key: "annotationScoreId", inverse_of: :annotation_queue_scores
  belongs_to :annotation_queue, foreign_key: "annotationQueueId", inverse_of: :annotation_queue_scores
end
