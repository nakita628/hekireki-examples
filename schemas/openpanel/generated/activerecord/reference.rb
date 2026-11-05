class Reference < ApplicationRecord
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :title, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :references
end
