class DocumentAnnotation < ApplicationRecord
  self.table_name = "DocumentAnnotation"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :pages, default: -> { [] }
  attribute :isVisible, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :title, presence: true
  validates :content, exclusion: { in: [ nil ] }

  belongs_to :document, foreign_key: "documentId", inverse_of: :annotations
  belongs_to :team, foreign_key: "teamId", inverse_of: :annotations
  belongs_to :created_by, class_name: "User", foreign_key: "createdById", inverse_of: :created_annotations
  has_many :images, class_name: "AnnotationImage", foreign_key: "annotationId", inverse_of: :annotation,
                    dependent: :destroy
end
