class GroupMembership < ApplicationRecord
  self.table_name = "GroupMembership"
  self.primary_key = %w[userId groupId]

  alias_attribute :created_at, :createdAt

  belongs_to :user, foreign_key: "userId", inverse_of: :group_memberships
  belongs_to :group, foreign_key: "groupId", inverse_of: :members
end
