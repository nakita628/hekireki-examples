class ProjectUsers < ApplicationRecord
  self.table_name = "ProjectUsers"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :role, { owner: "owner", member: "member", viewer: "viewer", billing: "billing" }, default: :member,
                                                                                          validate: true

  validates :userId, uniqueness: { scope: :projectId }

  belongs_to :user, foreign_key: "userId", inverse_of: :projects
  belongs_to :project, foreign_key: "projectId", inverse_of: :users
  has_one :notification_preference, foreign_key: "projectUserId", inverse_of: :project_user, dependent: :destroy
end
