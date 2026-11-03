class InvitationRecord < ApplicationRecord
  self.table_name = "invitation_record"
  self.inheritance_column = nil

  attribute :id, default: -> { Cuid.generate }

  validates :invitation_id, presence: true
  validates :type, presence: true
  validates :inviter, presence: true
  validates :accepter, presence: true
end
