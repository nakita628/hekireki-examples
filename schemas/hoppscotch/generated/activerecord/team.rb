class Team < ApplicationRecord
  self.table_name = "Team"

  attribute :id, default: -> { Cuid.generate }

  validates :name, presence: true

  has_many :collections, class_name: "TeamCollection", foreign_key: "teamID", inverse_of: :team, dependent: :destroy
  has_many :environments, class_name: "TeamEnvironment", foreign_key: "teamID", inverse_of: :team, dependent: :destroy
  has_many :invitations, class_name: "TeamInvitation", foreign_key: "teamID", inverse_of: :team, dependent: :destroy
  has_many :members, class_name: "TeamMember", foreign_key: "teamID", inverse_of: :team, dependent: :destroy
  has_many :requests, class_name: "TeamRequest", foreign_key: "teamID", inverse_of: :team, dependent: :destroy
end
