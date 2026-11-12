class Notes < ApplicationRecord
  self.inheritance_column = nil

  attribute :type, default: 0
  attribute :content, default: ""
  attribute :isArchived, default: false
  attribute :isRecycle, default: false
  attribute :isShare, default: false
  attribute :isTop, default: false
  attribute :isReviewed, default: false
  attribute :sharePassword, default: ""
  attribute :shareMaxView, default: 0
  attribute :shareViewCount, default: 0
  attribute :sortOrder, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :account, class_name: "Accounts", foreign_key: "accountId", optional: true, inverse_of: :notes
  has_many :attachments, class_name: "Attachments", foreign_key: "noteId", inverse_of: :note, dependent: :nullify
  has_many :tags, class_name: "TagsToNote", foreign_key: "noteId", inverse_of: :note, dependent: :restrict_with_error
  has_many :referenced_by, class_name: "NoteReference", foreign_key: "toNoteId", inverse_of: :to_note,
                           dependent: :restrict_with_error
  has_many :references, class_name: "NoteReference", foreign_key: "fromNoteId", inverse_of: :from_note,
                        dependent: :restrict_with_error
  has_many :comments, class_name: "Comments", foreign_key: "noteId", inverse_of: :note, dependent: :restrict_with_error
  has_many :histories, class_name: "NoteHistory", foreign_key: "noteId", inverse_of: :note, dependent: :destroy
  has_many :internal_shares, class_name: "NoteInternalShare", foreign_key: "noteId", inverse_of: :note,
                             dependent: :destroy
end
