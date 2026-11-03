class TeamMember < ApplicationRecord
  self.table_name = "TeamMember"

  attribute :id, default: -> { SecureRandom.uuid }

  enum :role, { owner: "OWNER", viewer: "VIEWER", editor: "EDITOR" }, validate: true

  validates :userUid, presence: true, uniqueness: { scope: :teamID }

  belongs_to :team, foreign_key: "teamID", inverse_of: :members
end
