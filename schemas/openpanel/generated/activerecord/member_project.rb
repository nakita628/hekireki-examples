class MemberProject < ApplicationRecord
  self.primary_key = %w[memberId projectId]

  alias_attribute :created_at, :createdAt

  enum :level, { read: "read", write: "write", admin: "admin" }, validate: { allow_nil: true }

  belongs_to :member, foreign_key: "memberId", inverse_of: :projects
  belongs_to :project, foreign_key: "projectId", inverse_of: :member_projects
end
