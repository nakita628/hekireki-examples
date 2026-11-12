class NoteInternalShare < ApplicationRecord
  self.table_name = "noteInternalShare"

  attribute :canEdit, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :noteId, uniqueness: { scope: :accountId }

  belongs_to :note, class_name: "Notes", foreign_key: "noteId", inverse_of: :internal_shares
  belongs_to :account, class_name: "Accounts", foreign_key: "accountId", inverse_of: :shared_notes
end
