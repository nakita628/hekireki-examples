defmodule Ghostfolio.TagsOnAccounts do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Ghostfolio.PrismaDateTime, autogenerate: {Ghostfolio.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          tag_id: Ecto.UUID.t(),
          account_id: String.t(),
          user_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          tag: Ghostfolio.Tag.t() | nil
        }

  schema "TagsOnAccounts" do
    field(:account_id, :string, primary_key: true, source: :accountId)
    field(:user_id, :string, primary_key: true, source: :userId)
    field(:tag_id, :binary_id, primary_key: true, source: :tagId)
    belongs_to(:tag, Ghostfolio.Tag, foreign_key: :tag_id, define_field: false, type: :binary_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end