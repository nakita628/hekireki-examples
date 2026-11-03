class UserDataroom < ApplicationRecord
  self.table_name = "UserDataroom"
  self.primary_key = %w[userId dataroomId]

  alias_attribute :created_at, :createdAt

  belongs_to :user, foreign_key: "userId", inverse_of: :user_datarooms
  belongs_to :team, foreign_key: "teamId", inverse_of: :user_datarooms
  belongs_to :dataroom, foreign_key: "dataroomId", inverse_of: :user_datarooms
end
