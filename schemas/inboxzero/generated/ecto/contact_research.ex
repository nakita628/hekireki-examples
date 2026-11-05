defmodule InboxZero.ContactResearch do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          research_started_at: DateTime.t(),
          found: boolean(),
          role: String.t() | nil,
          confidence: String.t() | nil,
          company: map() | nil,
          sources: [String.t()],
          inserted_at: DateTime.t()
        }

  schema "ContactResearch" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:research_started_at, InboxZero.PrismaDateTime, source: :researchStartedAt)
    field(:found, :boolean)
    field(:role, :string)
    field(:confidence, :string)
    field(:company, :map)
    field(:sources, {:array, :string})
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end