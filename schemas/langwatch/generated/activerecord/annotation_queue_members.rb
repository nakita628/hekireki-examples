class AnnotationQueueMembers < ApplicationRecord
  self.table_name = "AnnotationQueueMembers"
  self.primary_key = %w[annotationQueueId userId]

  belongs_to :user, foreign_key: "userId", inverse_of: :annotation_queues
  belongs_to :annotation_queue, foreign_key: "annotationQueueId", inverse_of: :members
end
