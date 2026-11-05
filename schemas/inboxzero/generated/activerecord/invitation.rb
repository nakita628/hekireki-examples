class Invitation < ApplicationRecord
  self.table_name = "invitation"

  attribute :id, default: -> { Cuid.generate }

  validates :email, presence: true
  validates :status, presence: true
  validates :expiresAt, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :invitations
  belongs_to :inviter, class_name: "EmailAccount", foreign_key: "inviterId", inverse_of: :invitations
end
