class TeamInvitation < ApplicationRecord
  self.table_name = "TeamInvitation"

  attribute :id, default: -> { Cuid.generate }

  enum :inviteeRole, { owner: "OWNER", viewer: "VIEWER", editor: "EDITOR" }, validate: true

  validates :creatorUid, presence: true
  validates :inviteeEmail, presence: true, uniqueness: { scope: :teamID }

  belongs_to :team, foreign_key: "teamID", inverse_of: :invitations
end
