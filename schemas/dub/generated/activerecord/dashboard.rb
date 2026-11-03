class Dashboard < ApplicationRecord
  self.table_name = "Dashboard"

  attribute :doIndex, default: false
  attribute :showConversions, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :linkId, uniqueness: { allow_nil: true }
  validates :folderId, uniqueness: { allow_nil: true }

  belongs_to :link, foreign_key: "linkId", optional: true, inverse_of: :dashboard
  belongs_to :folder, foreign_key: "folderId", optional: true, inverse_of: :dashboard
  belongs_to :project, foreign_key: "projectId", optional: true, inverse_of: :dashboards
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :dashboards
end
