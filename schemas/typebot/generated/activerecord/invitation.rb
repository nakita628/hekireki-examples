class Invitation < ApplicationRecord
  self.table_name = "Invitation"
  self.inheritance_column = nil

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { read: "READ", write: "WRITE", full_access: "FULL_ACCESS" }, validate: true

  validates :email, presence: true, uniqueness: { scope: :typebotId }

  belongs_to :typebot, foreign_key: "typebotId", inverse_of: :invitations
end
