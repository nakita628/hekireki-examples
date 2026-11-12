defmodule Blinko.Notes do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          type: integer(),
          content: String.t(),
          is_archived: boolean(),
          is_recycle: boolean(),
          is_share: boolean(),
          is_top: boolean(),
          is_reviewed: boolean(),
          share_password: String.t(),
          share_encrypted_url: String.t() | nil,
          share_expiry_date: DateTime.t() | nil,
          share_max_view: integer() | nil,
          share_view_count: integer() | nil,
          metadata: map() | nil,
          sort_order: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          account: Blinko.Accounts.t() | nil,
          attachments: [Blinko.Attachments.t()],
          tags: [Blinko.TagsToNote.t()],
          referenced_by: [Blinko.NoteReference.t()],
          references: [Blinko.NoteReference.t()],
          comments: [Blinko.Comments.t()],
          histories: [Blinko.NoteHistory.t()],
          internal_shares: [Blinko.NoteInternalShare.t()]
        }

  schema "notes" do
    field(:type, :integer, default: 0)
    field(:content, :string, default: "")
    field(:is_archived, :boolean, default: false, source: :isArchived)
    field(:is_recycle, :boolean, default: false, source: :isRecycle)
    field(:is_share, :boolean, default: false, source: :isShare)
    field(:is_top, :boolean, default: false, source: :isTop)
    field(:is_reviewed, :boolean, default: false, source: :isReviewed)
    field(:share_password, :string, default: "", source: :sharePassword)
    field(:share_encrypted_url, :string, source: :shareEncryptedUrl)
    field(:share_expiry_date, Blinko.PrismaDateTime, source: :shareExpiryDate)
    field(:share_max_view, :integer, default: 0, source: :shareMaxView)
    field(:share_view_count, :integer, default: 0, source: :shareViewCount)
    field(:metadata, :map)
    field(:sort_order, :integer, default: 0, source: :sortOrder)
    field(:account_id, :id, source: :accountId)
    belongs_to(:account, Blinko.Accounts, foreign_key: :account_id, define_field: false)
    has_many(:attachments, Blinko.Attachments, foreign_key: :note_id)
    has_many(:tags, Blinko.TagsToNote, foreign_key: :note_id)
    has_many(:referenced_by, Blinko.NoteReference, foreign_key: :to_note_id)
    has_many(:references, Blinko.NoteReference, foreign_key: :from_note_id)
    has_many(:comments, Blinko.Comments, foreign_key: :note_id)
    has_many(:histories, Blinko.NoteHistory, foreign_key: :note_id)
    has_many(:internal_shares, Blinko.NoteInternalShare, foreign_key: :note_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end