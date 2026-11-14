class Topic < ApplicationRecord
  self.table_name = "Topic"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :automaticallyGenerated, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :embeddings_model, presence: true
  validates :centroid, exclusion: { in: [ nil ] }
  validates :p95Distance, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :topics
  belongs_to :parent, class_name: "Topic", foreign_key: "parentId", optional: true, inverse_of: :subtopics
  has_many :subtopics, class_name: "Topic", foreign_key: "parentId", inverse_of: :parent,
                       dependent: :restrict_with_error
end
