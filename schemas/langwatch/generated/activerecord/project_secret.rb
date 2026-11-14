class ProjectSecret < ApplicationRecord
  self.table_name = "ProjectSecret"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, uniqueness: { scope: :projectId }
  validates :encryptedValue, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :project_secrets
  belongs_to :created_by, class_name: "User", foreign_key: "createdById", inverse_of: :secrets_created
  belongs_to :updated_by, class_name: "User", foreign_key: "updatedById", inverse_of: :secrets_updated
end
