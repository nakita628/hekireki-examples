class AnnotationImage < ApplicationRecord
  self.table_name = "AnnotationImage"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :filename, presence: true
  validates :url, presence: true

  belongs_to :annotation, class_name: "DocumentAnnotation", foreign_key: "annotationId", inverse_of: :images
end
