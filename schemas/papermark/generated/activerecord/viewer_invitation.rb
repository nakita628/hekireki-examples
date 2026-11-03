class ViewerInvitation < ApplicationRecord
  self.table_name = "ViewerInvitation"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  enum :status, { sent: "SENT", failed: "FAILED", bounced: "BOUNCED" }, default: :sent, validate: true

  validates :invitedBy, presence: true

  belongs_to :viewer, foreign_key: "viewerId", inverse_of: :invitations
  belongs_to :link, foreign_key: "linkId", inverse_of: :viewer_invitations
  belongs_to :group, class_name: "ViewerGroup", foreign_key: "groupId", optional: true, inverse_of: :invitations
end
