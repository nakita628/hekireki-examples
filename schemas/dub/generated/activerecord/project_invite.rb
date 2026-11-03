class ProjectInvite < ApplicationRecord
  self.table_name = "ProjectInvite"

  alias_attribute :created_at, :createdAt

  enum :role, { owner: "owner", member: "member", viewer: "viewer", billing: "billing" }, default: :member,
                                                                                          validate: true

  validates :email, presence: true, uniqueness: { scope: :projectId }
  validates :expires, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :invites
end
